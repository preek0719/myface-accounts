import React, {createContext, ReactNode, useState} from "react";

export const LoginContext = createContext({
    isLoggedIn: false,
    isAdmin: false,
    authHeader: "",
    logIn: (username: string, password: string) => {},
    logOut: () => {},
});

interface LoginManagerProps {
    children: ReactNode
}

export function LoginManager(props: LoginManagerProps): JSX.Element {
    const [loggedIn, setLoggedIn] = useState(false);
    const [authHeader, setAuthHeader] = useState("");
    
    function logIn(username: string, password: string) {
        var headerString = (btoa(`${username}:${password}`))
        setAuthHeader(`Basic ${headerString}`);

        
        setLoggedIn(true);
    }
    
    function logOut() {
        setAuthHeader("");
        setLoggedIn(false);
    }
    
    const context = {
        isLoggedIn: loggedIn,
        isAdmin: loggedIn,
        authHeader: authHeader,
        logIn: logIn,
        logOut: logOut,
    };
    
    return (
        <LoginContext.Provider value={context}>
            {props.children}
        </LoginContext.Provider>
    );
}