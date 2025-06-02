/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./app/**/*.{js,ts,jsx,tsx,mdx}", // Para projetos Next.js
    "./pages/**/*.{js,ts,jsx,tsx,mdx}", // Para projetos Next.js
    "./components/**/*.{js,ts,jsx,tsx,mdx}", // Se você tem uma pasta components
    "./src/**/*.{js,ts,jsx,tsx,mdx}", // Se sua aplicação está em src (comum em Create React App)
  ],
  theme: {
    extend: {},
  },
  plugins: [],
}