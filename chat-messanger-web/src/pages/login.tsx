import { useEffect, useState } from "react";
import toast, { Toaster } from "react-hot-toast";
import "../styles/globals.css";
import { loginUser } from "@/services/ChatMessagerApi";
import router from "next/router";
import {useUser } from "@/context/userContext";

export default function Login() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const { getLoggedUser, setLoggedUser } = useUser();

  useEffect(() => {
    console.log("Checking if user is already logged in...", getLoggedUser());
    if(getLoggedUser()) {
      router.replace('/home');
    }

  }, []);

  const authenticateUser = async (e: React.FormEvent) => {
    e.preventDefault();

    // Simulate an asynchronous authentication call
    var fetchData = await loginUser({ email: email, password: password });

    if (!fetchData) {
      toast.error("Login failed. Please try again.");
      return;
    }

  
   setLoggedUser(fetchData.data);
 
    router.replace('/home');
    
    toast.success("Welcome! Redirecting to dashboard..."),
    console.log("Authenticating user...");
    console.log("email:", email);
    console.log("Password:", password);
  };

  return (
    <>
      <div className="min-h-screen flex items-center justify-center bg-gray-100 p-4">
        <div className="bg-white p-8 rounded-lg shadow-lg max-w-md w-full">
          <h1 className="text-3xl font-bold text-center text-gray-800 mb-8">
            Welcome Back!
          </h1>
          <form onSubmit={authenticateUser} className="space-y-6">
            <div>
              <label
                htmlFor="username"
                className="block text-sm font-medium text-gray-700 mb-1"
              >
                Username
              </label>
              <input
                id="username"
                value={email}
                type="text"
                onChange={(e) => setEmail(e.target.value)}
                required
                className="mt-1 block w-full px-4 py-2 border border-gray-300 rounded-md shadow-sm focus:ring-blue-500 focus:border-blue-500 sm:text-sm"
                placeholder="Enter your username"
              />
            </div>
            <div>
              <label
                htmlFor="password"
                className="block text-sm font-medium text-gray-700 mb-1"
              >
                Password
              </label>
              <input
                id="password"
                type="password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                required
                className="mt-1 block w-full px-4 py-2 border border-gray-300 rounded-md shadow-sm focus:ring-blue-500 focus:border-blue-500 sm:text-sm"
                placeholder="Enter your password"
              />
            </div>
            <button
              type="submit"
              className="w-full flex justify-center py-2 px-4 border border-transparent rounded-md shadow-sm text-sm font-medium text-white bg-blue-600 hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500 transition duration-150 ease-in-out"
            >
              Login
            </button>
          </form>
          <p className="mt-6 text-center text-sm text-gray-600">
            Don't have an account?{" "}
            <a
              href="#"
              className="font-medium text-blue-600 hover:text-blue-500"
            >
              Sign up here
            </a>
          </p>
        </div>
      </div>
    </>
  );
}
