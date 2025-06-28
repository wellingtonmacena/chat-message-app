import ChatMessageComponent from "@/components/ChatMessageComponent";
import UsersSideBarComponent from "../components/UsersSideBarComponent";
import "../styles/globals.css";
import { useEffect, useRef, useState } from "react";
import User from "@/models/user.interface";
import * as signalR from "@microsoft/signalr";

import { environment } from "@/environments/environments";
import Message from "@/models/message.interface";
import { useUser } from "@/context/userContext";

export default function Home() {
  const [selectedRecipientUser, setselectedRecipientUser] =
    useState<User | null>(null);
  const connectionRef = useRef<signalR.HubConnection | null>(null);
  const { getLoggedUser } = useUser();
  let myUser: User;

  useEffect(() => {
    myUser = getLoggedUser()!;
    const conn = new signalR.HubConnectionBuilder()
      .withUrl(
        `${environment.CHAT_MESSAGE_API_BASE_URL_WEB_CONNECTION}?userId=${myUser.id}`
      )
      .withAutomaticReconnect()
      .build();

    conn
      .start()
      .then(() => {
        console.log("Conectado ao SignalR");
      })
      .catch((err) => {
        console.error("Erro ao conectar ao SignalR:", err);
      });

    connectionRef.current = conn;

    return () => {
      conn.stop();
    };
  }, []);

  return (
    <div className="flex h-screen">
      <UsersSideBarComponent onSelectRecipientUser={setselectedRecipientUser} connectionRef={connectionRef.current} />
      {selectedRecipientUser ? (
        <ChatMessageComponent
          recipient={selectedRecipientUser}
          connectionRef={connectionRef.current}
        />
      ) : (
        <div className="flex-1 flex items-center justify-center text-gray-500">
          Selecione um usuário para iniciar a conversa
        </div>
      )}
    </div>
  );
}
