
export default interface GetMessagesRequest {

    userId: string; // The ID of the user whose messages are being requested
    recipientId?: string; // Optional: The ID of the recipient to filter messages by
    pageNumber?: number; // Optional: The page number for pagination
    pageSize?: number; // Optional: The number of messages per page for pagination
} 