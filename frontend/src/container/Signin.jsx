import { useState } from "react";
import { FcGoogle } from "react-icons/fc";
import { MdOutlineClose } from "react-icons/md";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";

// eslint-disable-next-line react/prop-types
const Signin = ({signin, setSignin, signup, setSignup}) => {
  const navigate = useNavigate();

  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [keepSignedIn, setKeepSignedIn] = useState(false);
  const [loading, setLoading] = useState(false);

  const submitInputs = async (e) => {
    e.preventDefault();

    if (email.trim() === "" || password.trim() === "") {
      toast.warning("Please fill the details");
      return;
    }

    if (!email.includes("@")) {
      toast.warning("Please enter a valid email address");
      return;
    }

    if (password.length < 6) {
      toast.warning("Password must be at least 6 characters long");
      return;
    }

    setLoading(true);

    try {
      const apiBase = import.meta.env.VITE_API_URL || "/api";
      const response = await fetch(`${apiBase}/auth/login`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ email, password }),
      });

      const data = await response.json();
      if (!response.ok) {
        toast.error(data.message || "Sign in failed");
        return;
      }

      const storage = keepSignedIn ? localStorage : sessionStorage;
      storage.setItem("token", data.token);
      storage.setItem("user", data.user);
      toast.success(data.message || "Sign in successful");
      setSignin(false);
      navigate("/");
    } catch (err) {
      toast.error("Unable to sign in. Please try again.");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="absolute top-36 right-0 left-0 m-auto z-20 bg-[#FFFFFF] shadowCard w-[310px] sm:w-[468px] md:w-[568px] rounded px-8 py-6 flex flex-col  gap-6 scaleUp">
      <header className="flex flex-col justify-start">
        <div className="flex items-center justify-between">
          <h1 className="text-[#6E7491] text-[20px] sm:text-[24px] leading-5 sm:leading-8 font-bold ">
            Sign in for Tripma
          </h1>
          <MdOutlineClose
            className="text-[#6E7491] cursor-pointer"
            onClick={() => setSignin(!signin)}
          />
        </div>
      </header>
      <form className="flex flex-col gap-4">
        <input
          type="email"
          placeholder="Email address"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          className="outline-none border-[1px] border-[#A1B0CC] p-2 placeholder:text-[#7C8DB0] text-[#7C8DB0] rounded"
        />
        <input
          type="password"
          placeholder="Password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          className="outline-none border-[1px] border-[#A1B0CC] p-2 placeholder:text-[#7C8DB0] text-[#7C8DB0] rounded"
        />
      </form>
      <div className="flex flex-col gap-2">
        <form className="flex items-center gap-2 ">
          <input
            type="checkbox"
            name="checkbox"
            id="checkbox"
            checked={keepSignedIn}
            onChange={(e) => setKeepSignedIn(e.target.checked)}
            className="text-[#7C8DB0] outline-none "
          />
          <label htmlFor="checkbox" className="text-[#7C8DB0] text-sm sm:text-base">
            Keep me signed in
          </label>
        </form>
      </div>
      <div className="w-full flex items-center justify-center">
        <button
          className="w-full bg-[#605DEC] text-[#FAFAFA] rounded py-3 outline-none border-none disabled:opacity-60"
          onClick={submitInputs}
          disabled={loading}
        >
          {loading ? "Signing in..." : "Sign In"}
        </button>
      </div>
      <div className="w-full flex items-center justify-center">
        <p className="text-[#7C8DB0] text-sm sm:text-base text-center">
          Don't have an account?{" "}
          <span
            className="text-[#605DEC] cursor-pointer"
            onClick={() => {
              setSignin(false);
              setSignup(true);
            }}
          >
            Sign up
          </span>
        </p>
      </div>  
      <div className="flex items-center justify-center gap-2">
        <div className="w-full text-[#A1B0CC] border-t-[1px] border-t-[#A1B0CC] h-1 " />
        <p className="text-[#7C8DB0] text-[18px] leading-6">or</p>
        <div className="w-full text-[#A1B0CC] border-t-[1px] border-t-[#A1B0CC] h-1" />
      </div>
      <div className="w-full flex items-center justify-center">
        <button
          className="w-full flex gap-2 items-center justify-center border-[1px] border-[#605DEC] rounded p-3"
          onClick={() => setSignin(false)}
        >
          <FcGoogle className="w-[18px] h-[18px]" />
          <p className="text-[#605CDE] text-[16px] leading-6">
            Continue with Google
          </p>
        </button>
      </div>
    </div>
  );
};

export default Signin;
