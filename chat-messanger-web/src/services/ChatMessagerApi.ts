

import toast from "react-hot-toast";

import User from "../models/user.interface";
import LoginRequest from "@/models/dto/loginRequest.interface";
import GetMessagesRequest from "@/models/dto/getMessagesRequest.interface";
import SendMessageRequest from "@/models/sendMessageRequest";

const BASE_URL = "http://localhost:5201/api/v1";

export const getUsers=async ()=>{
  return await fetch(`${BASE_URL}/users`);
}


export const loginUser = async (loginRequest: LoginRequest) => {
    try {
        const response = await fetch(`${BASE_URL}/auth/login`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(loginRequest),
        });
    
        if (!response.ok) {
        throw new Error("Login failed");
        }
    
        const data = await response.json();
        return data;
    } catch (error) {
        toast.error("Login failed. Please try again.");
        console.error("Error during login:", error);
        throw error;
    }
}

export const getMessages = async ({userId, pageNumber, pageSize, recipientId}:GetMessagesRequest) => {

    try {
        const response = await fetch(`${BASE_URL}/users/${userId}/messages?recipientId=${recipientId}&pageNumber=${pageNumber}&pageSize=${pageSize}`, {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
            },
        });

        if (!response.ok) {
            throw new Error("Failed to fetch messages");
        }

        const data = await response.json();
        return data;
    } catch (error) {
        toast.error("Failed to fetch messages. Please try again.");
        console.error("Error fetching messages:", error);
        throw error;
    }

   
}

export const sendMessage = async (sendMessageRequest:SendMessageRequest) => {
    try {
        const response = await fetch(`${BASE_URL}/messages`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(sendMessageRequest),
        });

        if (!response.ok) {
            throw new Error("Failed to send message");
        }

        const data = await response.json();
        return data;
    } catch (error) {
        toast.error("Failed to send message. Please try again.");
        console.error("Error sending message:", error);
        throw error;
    }
}