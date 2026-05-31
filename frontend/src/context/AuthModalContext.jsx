import { createContext, useState } from "react";

export const AuthModalContext = createContext({
  signin: false,
  signup: false,
  setSignin: () => {},
  setSignup: () => {},
});

export const AuthModalProvider = ({ children }) => {
  const [signin, setSignin] = useState(false);
  const [signup, setSignup] = useState(false);

  return (
    <AuthModalContext.Provider value={{ signin, signup, setSignin, setSignup }}>
      {children}
    </AuthModalContext.Provider>
  );
};
