"use client";
import { json } from "stream/consumers";
import User from "../models/user.interface";
import React, {
  createContext,
  useState,
  ReactNode,
  useContext,
  useEffect,
} from "react";


interface UserContextType {
  getLoggedUser: ()=> User | null;
  users: User[];
  setLoggedUser: (user: User) => void;
}

const UserContext = createContext<UserContextType | undefined>(undefined);

export const useUser = () => {
  const context = useContext(UserContext);
  if (!context) {
    throw new Error("useUser must be used within a UserProvider");
  }
  return context;
};


export const UserProvider: React.FC<{ children: ReactNode }> = ({
  children,
}) => {
  const [loggedUser, setLoggedUserState] = useState<User | null>(null);
  const [users, setUsers] = useState<User[]>([]);

 
  const getLoggedUser = () => {
    var g = localStorage.getItem("loggedUser")!;
     return JSON.parse( g);
  };

  const setLoggedUser = (user: User) => {
    localStorage.setItem("loggedUser", JSON.stringify(user))!;
     setLoggedUserState(user);
  };

  return (
    <UserContext.Provider value={{ getLoggedUser, users, setLoggedUser }}>
      {children}
    </UserContext.Provider>
  );
};

