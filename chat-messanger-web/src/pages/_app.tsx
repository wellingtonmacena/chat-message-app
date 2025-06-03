import '../styles/globals.css';
import { AppProps } from 'next/app';
import { UserProvider } from '../context/userContext'

import { Toaster } from 'react-hot-toast';

function MyApp({ Component, pageProps }: AppProps) {
  return (
    <UserProvider>

        <Component {...pageProps} />
        <Toaster position='top-right'/>

    </UserProvider>
  );
}

export default MyApp;
