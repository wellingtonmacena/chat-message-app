import { useEffect, useState } from "react";
import User from "../../models/user.interface";

interface Props {
  onSelectRecipientUser: (user: User) => void;
}

export default function UsersSideBarComponent({
  onSelectRecipientUser,
}: Props) {
  const [users, setUsers] = useState<User[]>([
    { isOnline: false, id: "3", name: "Diana Olivia Ross", profilePictureUrl: "https://i.pravatar.cc/40?img=3" },
    { isOnline: true, id: "1", name: "Alice wellington amcena well", profilePictureUrl: "https://i.pravatar.cc/40?img=1" },

    { isOnline: true, id: "4", name: "Charlie" , profilePictureUrl: "https://i.pravatar.cc/40?img=4" },
      { isOnline: false, id: "2", name: "Bob" , profilePictureUrl: "https://i.pravatar.cc/40?img=2" },
    
  ]);
  
useEffect(() => {
    setUsers((prev) =>
      [...prev].sort((a, b) => {
        if (a.isOnline && !b.isOnline) return -1;
        if (!a.isOnline && b.isOnline) return 1;
        return a.name.localeCompare(b.name);
      })
    );
  }, []);

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
