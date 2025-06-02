import User from "../models/user.interface";
import React, {
  createContext,
  useState,
  ReactNode,
  useContext,
  useEffect,
} from "react";


interface UserContextType {
  user: User | null;
  users: User[];
  setUser: (user: User) => void;
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
  const [user, setUserState] = useState<User | null>(null);
  const [users, setUsers] = useState<User[]>([]);

  useEffect(() => {
    const fetchUsers = async () => {

      
    };

    fetchUsers();
  }, []);

  const setUser = (user: User) => {
     setUserState(user);
  };

  return (
    <UserContext.Provider value={{ user, users, setUser }}>
      {children}
    </UserContext.Provider>
  );
};

