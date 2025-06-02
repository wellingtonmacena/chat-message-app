
export default interface User {
    id: string;
    name: string;
    email?: string;
    profilePictureUrl?: string;
    createdAt?: Date;
    updatedAt?: Date;
    isOnline: boolean;
}