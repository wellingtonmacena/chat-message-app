import { useState } from "react";
import toast, { Toaster } from "react-hot-toast";
import "../styles/globals.css";

export default function Login() {
  const [user, setUser] = useState("");
  const [password, setPassword] = useState("");

  const authenticateUser = async (e: React.FormEvent) => {
    e.preventDefault();

    // Simulate an asynchronous authentication call
    var fetchData = new Promise<boolean>((resolve, reject) => {
      setTimeout(() => {
        // Simulate a 50% chance of success
        var randomBoolean = Math.random();
        if (randomBoolean < 0.5) {
          resolve(true); // User authenticated successfully
        } else {
          reject(new Error("Invalid username or password")); // Authentication failed
        }
      }, 1500); // Simulate a 1.5-second network request
    });

   
    toast.promise(
      fetchData,
      {
        loading: "Verifying credentials...",
        success: "Welcome! Redirecting to dashboard...",
        error: (err) => `Login failed: ${err.message}`,
      }
    );

    console.log("Authenticating user...");
    console.log("Username:", user);
    console.log("Password:", password);
  };

  return (
    <>
    <Toaster/>
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
              value={user}
              type="text"
              onChange={(e) => setUser(e.target.value)}
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
          <a href="#" className="font-medium text-blue-600 hover:text-blue-500">
            Sign up here
          </a>
        </p>
      </div>
    </div>
    </>
  );
}