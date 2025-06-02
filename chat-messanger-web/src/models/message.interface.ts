

export default interface Message {
  id: string;
  
    createdAt: Date;
    recipientId: string;
    senderId: string;
    content: string;
}
