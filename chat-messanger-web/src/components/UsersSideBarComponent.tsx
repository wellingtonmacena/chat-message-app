import { useEffect, useState } from "react";
import User from "../models/user.interface";
import { useUser } from "@/context/userContext";
import { getUsers } from "@/services/ChatMessagerApi";

interface Props {
  onSelectRecipientUser: (user: User) => void;
    connectionRef: signalR.HubConnection | null;
}

export default function UsersSideBarComponent({
  onSelectRecipientUser,
  connectionRef,
}: Props) {
  const { getLoggedUser, setLoggedUser } = useUser();

  const [users, setUsers] = useState<User[]>([]);

useEffect(() => {
  const connection = connectionRef;
  if (!connection) return;

  // Evento de desconexão
  connection.onclose((error) => {
    console.log("Desconectado do SignalR", error);
  });

  // Evento para atualizar status do usuário
  connection.on("RemoveUser", (userId: string) => {
    console.log("Usuário removido:", userId);

    setUsers((prevUsers) => {
      const updatedUsers = prevUsers.map((user) =>
        user.id === userId ? { ...user, isOnline: false } : user
      );

      return sortUsersByOnlineStatusAndName(updatedUsers);
    });
  });

 
}, []);


  useEffect(() => {
    const fetchUsers = async () => {
      console.log("Current user:", getLoggedUser());

      try {
        const response = await getUsers();
        console.log("Fetched users:", response.data);
        // Verifica se response.body existe e é um array antes de setar
        if (Array.isArray(response?.data)) {
          var index = response.data.findIndex(
            (item) => item.id === getLoggedUser()?.id
          );
          if (index !== -1) {
            response.data.splice(index, 1); // Remove o usuário logado da lista
          }
          var sortedUsers = sortUsersByOnlineStatusAndName(response.data);
          setUsers([...sortedUsers]);
        } else {
          console.error("Resposta inesperada de getUsers:", response);
        }
      } catch (error) {
        console.error("Failed to fetch users:", error);
      }
    };

    fetchUsers();
  }, []);

  const sortUsersByOnlineStatusAndName = (users: User[]) => {
  return users.sort((a, b) => {
    // Primeiro: usuários online antes de offline
    if (a.isOnline && !b.isOnline) return -1;
    if (!a.isOnline && b.isOnline) return 1;

    // Segundo: ordenar por nome (alfabética)
    return a.name.localeCompare(b.name);
  });
};

  return (
    <aside className="w-54 h-screen bg-gray-100 border-r border-gray-300 p-4">
      <h2 className="text-xl font-semibold mb-4">Users</h2>
      <ul className="space-y-2">
        {users.map((user, index) => (
          <li
            key={index}
            className="flex items-center gap-2 p-2 border-b border-gray-200 hover:bg-gray-200 cursor-pointer text-gray-800"
            onClick={() => onSelectRecipientUser(user)}
          >
            {/* Online indicator */}
            <span
              className={`w-3 h-3 rounded-full ${
                user.isOnline ? "bg-green-500" : "bg-gray-400"
              }`}
              title={user.isOnline ? "Online" : "Offline"}
            />
            {user.name.length > 15
              ? user.name.substring(0, 16) + "..."
              : user.name}
          </li>
        ))}
      </ul>
    </aside>
  );
}
