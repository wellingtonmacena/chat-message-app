import toast from "react-hot-toast";

import User from "../models/user.interface";
import LoginRequest from "@/models/dto/loginRequest.interface";
import GetMessagesRequest from "@/models/dto/getMessagesRequest.interface";
import SendMessageRequest from "@/models/sendMessageRequest";
import axios from "axios";

const BASE_URL = "https://localhost:7055/api/v1";

export const getUsers = async () => {
  return await axios.get(`${BASE_URL}/users`);
};

export const loginUser = async (loginRequest: LoginRequest) => {
  try {
    console.log("Login request:", JSON.stringify(loginRequest));
    const response = await axios.post(`${BASE_URL}/users/login`, loginRequest);

    console.log("response:", response);
    return response;
  } catch (error) {
    toast.error("Login failed. Please try again.");
  }
};

export const getMessages = async ({
  userId,
  recipientId,
  pageNumber,
  pageSize,
  
}: GetMessagesRequest) => {
  try {
    const response = await axios.get(
      `${BASE_URL}/messages?senderId=${userId}&recipientId=${recipientId}&pageNumber=${pageNumber}&pageSize=${pageSize}`,
      
    );
    const data = response.data.items;
    return data;
  } catch (error) {
    toast.error("Failed to fetch messages. Please try again.");
    console.error("Error fetching messages:", error);
    throw error;
  }
};

export const sendMessage = async (sendMessageRequest: SendMessageRequest) => {
  try {
    const response = await axios.post(`${BASE_URL}/messages`, sendMessageRequest, );

    if (!response.status || response.status !== 200) {
      console.error("Failed to send message:", response);
     
    }

    const data = response.data;
    toast.success("Message sent successfully!");
    return data;
  } catch (error) {
    toast.error("Failed to send message. Please try again.");
    console.error("Error sending message:", error);
    throw error;
  }
};
