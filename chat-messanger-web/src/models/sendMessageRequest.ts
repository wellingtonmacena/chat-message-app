

export default interface SendMessageRequest {
    senderId: string; // The ID of the user sending the message
    recipientId: string; // The ID of the user receiving the message
    content: string; // The content of the message being sent
    createdAt?: Date; // Optional: The date and time when the message was created, defaults to current time if not provided
}