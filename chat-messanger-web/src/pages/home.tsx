import ChatMessageComponent from "@/components/ChatMessageComponent";
import UsersSideBarComponent from "../components/UsersSideBarComponent";
import "../styles/globals.css";
import { useState } from "react";
import User from "@/models/user.interface";

export default function Home() {
  const [selectedRecipientUser, setselectedRecipientUser] =
    useState<User | null>(null);

  return (
    <div className="flex h-screen">
      <UsersSideBarComponent onSelectRecipientUser={setselectedRecipientUser} />
      {selectedRecipientUser ? (
        <ChatMessageComponent recipient={selectedRecipientUser} />
      ) : (
        <div className="flex-1 flex items-center justify-center text-gray-500">
          Selecione um usuário para iniciar a conversa
        </div>
      )}
    </div>
  );
}
