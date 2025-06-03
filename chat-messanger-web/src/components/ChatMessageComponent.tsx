import { useUser } from "@/context/userContext";
import Message from "@/models/message.interface";
import User from "@/models/user.interface";
import { getMessages, sendMessage } from "@/services/ChatMessagerApi";
import { use, useEffect, useRef, useState } from "react";
import { v4 as uuidv4, v4 } from "uuid";

interface Props {
  recipient: User;
}

export default function ChatMessageComponent({ recipient }: Props) {
  const [newMessage, setNewMessage] = useState("");
  const [currentPage, setCurrentPage] = useState(1);
 const bottomRef = useRef<HTMLDivElement>(null);
  const [isFetching, setIsFetching] = useState(false);
  const { getLoggedUser, setLoggedUser } = useUser();
  let myUser: User = getLoggedUser()!;

  const [messages, setMessages] = useState<Message[]>([]);

  useEffect(() => {
    const fetchMessages = async () => {
      const response = await getMessages({
        userId: myUser.id,
        recipientId: recipient.id,
        pageNumber: currentPage,
        pageSize: 10,
      });

      console.log("Fetched messages:", response);
      setMessages(response);
    };

    fetchMessages();
  }, [recipient]);

 

  const fetchMoreMessages = () => {
    if (isFetching) return;
    setIsFetching(true);
  };

  useEffect(() => {
    const observer = new IntersectionObserver(
      (entries) => {
        const [entry] = entries;
        if (entry.isIntersecting) {
          fetchMoreMessages();
        }
      },
      {
        root: null,
        rootMargin: "0px",
        threshold: 1.0,
      }
    );

    if (bottomRef.current) {
      observer.observe(bottomRef.current);
    }

    return () => {
      if (bottomRef.current) observer.unobserve(bottomRef.current);
    };
  }, [bottomRef.current, isFetching]);

  // Scroll suave para o final quando as mensagens forem atualizadas
  // Novo useEffect para lidar com o scroll após a atualização das mensagens
  useEffect(() => {
    if (bottomRef.current) {
      bottomRef.current.scrollIntoView({
        behavior: "smooth",
        block: "end", // Ensures the end of the element is in view
      });

      setCurrentPage((prev) => prev + 1); // Incrementa a página após o scroll
    }
  }, [messages]); // Dispara este efeito sempre que 'messages' é atualizado

  const handleSendMessage = async () => {
    if (!newMessage.trim()) return;

    const messageToAdd: Message = {
      id: v4(), // Gera um ID único para a mensagem
      createdAt: new Date(),
      recipientId: recipient.id,
      senderId: myUser.id,
      content: newMessage.trim(),
    };

    await sendMessage(messageToAdd);
    setMessages((prev) => [...prev, messageToAdd]);

    setNewMessage("");
    // A chamada para scrollIntoView foi movida para o useEffect acima.
  };

  return (
    <div className="flex flex-col min-h-screen bg-white w-285">
      {/* Topo com nome, status e foto do recipient */}
      <header className="flex items-center gap-4 p-4 border-b bg-gray-50 shadow-sm">
        {recipient.profilePictureUrl && (
          <img
            src={recipient.profilePictureUrl}
            alt={`${recipient.name} profile`}
            className="w-12 h-12 rounded-full object-cover shadow-md"
          />
        )}
        <div>
          <h2 className="text-xl font-semibold text-gray-900">
            {recipient.name}
          </h2>
          <p
            className={`text-sm font-medium ${
              recipient.isOnline ? "text-green-500" : "text-gray-400"
            }`}
          >
            {recipient.isOnline ? "Online" : "Offline"}
          </p>
        </div>
      </header>

      {/* Mensagens */}
      <main className="flex-1 p-6 overflow-y-auto space-y-6 bg-gradient-to-b from-gray-100 to-white">
        {messages.map((msg) => {
          const isMe = msg.senderId === myUser.id;
          const sender = isMe ? myUser : recipient;

          return (
            <div
              key={msg.id}
              className={`flex items-end max-w-lg ${
                isMe ? "ml-auto flex-row-reverse" : "mr-auto"
              } gap-3`}
            >
              {/* Foto do remetente */}
              <img
                src={
                  sender.profilePictureUrl ?? "https://i.pravatar.cc/40?img=3"
                }
                alt={`${sender.name} avatar`}
                className="w-9 h-9 rounded-full object-cover shadow-sm"
              />

              {/* Balão de mensagem com "tail" */}
              <div
                className={`relative px-5 py-3 rounded-2xl text-sm leading-snug shadow-md
                  ${
                    isMe
                      ? "bg-blue-600 text-white"
                      : "bg-gray-200 text-gray-900"
                  }
                `}
              >
                {/* "Tail" do balão */}
                <div
                  className={`absolute top-2 ${
                    isMe ? "right-full" : "left-full"
                  } w-0 h-0 border-t-8 border-b-8 border-transparent
                  ${isMe ? "border-r-blue-600" : "border-l-gray-200"}`}
                  aria-hidden="true"
                ></div>

                <div className="font-semibold mb-1">
                  {isMe ? "Você" : sender.name}
                </div>
                <div>{msg.content}</div>
                <div className="text-xs text-right opacity-60 mt-1 font-mono">
                  {new Date(msg.createdAt).toLocaleTimeString([], {
                    hour: "2-digit",
                    minute: "2-digit",
                  })}
                </div>
              </div>
            </div>
          );
        })}

        {/* Loader ou Trigger */}
        <div
          ref={bottomRef}
          className="flex justify-center items-center text-gray-500 text-xs font-medium select-none"
        >
          {isFetching ? "Carregando..." : "Desça para carregar mais"}
        </div>
      </main>

      <footer className="bg-white border-t shadow-md p-4">
        <div className="flex items-center gap-3">
          <input
            type="text"
            placeholder="Digite sua mensagem..."
            className="flex-1 px-4 py-2 rounded-full border border-gray-300 shadow-sm focus:outline-none focus:ring-2 focus:ring-blue-500"
            value={newMessage}
            onChange={(e) => setNewMessage(e.target.value)}
            onKeyDown={(e) => {
              if (e.key === "Enter") handleSendMessage();
            }}
          />
          <button
            onClick={handleSendMessage}
            className="px-4 py-2 bg-blue-600 text-white rounded-full hover:bg-blue-700 transition"
          >
            Enviar
          </button>
        </div>
      </footer>
    </div>
  );
}
