import { useContext, useEffect, useState } from "react";
import { useNavigate, useSearchParams } from "react-router-dom";
import { map } from "../assets/images";
import { united } from "../assets/logo";
import { FlightCard, PriceDetails, PriceGraph } from "../container";
import { AuthModalContext } from "../context/AuthModalContext";

const FlightChoose = () => {
  const [priceShown, setPriceShow] = useState(true);
  const [searchParams] = useSearchParams();
  const [flights, setFlights] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");
  const [selectedFlight, setSelectedFlight] = useState(null);
  const { setSignin, setSignup } = useContext(AuthModalContext);
  const navigate = useNavigate();

  const from = searchParams.get("from") || "SFO";
  const to = searchParams.get("to") || "NRT";

  useEffect(() => {
    const controller = new AbortController();

    const searchFlights = async () => {
      setLoading(true);
      setError("");

      try {
        const apiBase = import.meta.env.VITE_API_URL || "/api";
        const response = await fetch(
          `${apiBase}/flight/search/${encodeURIComponent(from)}/${encodeURIComponent(to)}`,
          { signal: controller.signal }
        );

        if (response.status === 404) {
          setFlights([]);
          return;
        }

        if (!response.ok) {
          throw new Error("Error occurred while calling the flight search API.");
        }

        const data = await response.json();
        setFlights(data);
      } catch (err) {
        if (err.name !== "AbortError") {
          setError("Cannot connect to the flight search API.");
          setFlights([]);
        }
      } finally {
        setLoading(false);
      }
    };

    searchFlights();
    return () => controller.abort();
  }, [from, to]);

  return (
    <>
      <div className="flex lg:flex-row flex-col items-start justify-between gap-16 ">
        <div className="w-full lg:w-[872px] h-full flex flex-col gap-5">
          <div className="flex items-start justify-start">
            <h1 className="text-[#6E7491]  text-lg leading-6 font-semibold">
              Choose a <span className="text-[#605DEC]">departing </span>/{" "}
              <span className="text-[#605DEC]">returning </span>flight
            </h1>
          </div>
          <div className="w-full flex flex-col items-start justify-start  border-[1px] border-[#E9E8FC] rounded-xl">
            {loading ? (
              <div className="p-8 text-center text-[#605DEC]">
                Loading flights from <strong>{from}</strong> to <strong>{to}</strong>...
              </div>
            ) : error ? (
              <div className="p-8 text-center text-red-500">{error}</div>
            ) : flights.length === 0 ? (
              <div className="p-8 text-center text-[#7C8DB0]">
                No flights found from <strong>{from}</strong> to <strong>{to}</strong>.
              </div>
            ) : (
              flights.map((flight) => (
                <div
                  key={flight.id}
                  className="w-full cursor-pointer border-b-[1px] border-[#E9E8FC] hover:bg-[#F6F6FE] transition-all duration-300 focus:bg-[#F6F6FE]"
                  onClick={() => {
                    setPriceShow(false);
                    setSelectedFlight(flight);
                  }}
                >
                  <FlightCard
                    img={united}
                    stops={`${flight.from} → ${flight.to}`}
                    name={flight.flightNumber}
                    time={`${new Date(flight.departureTime).toLocaleString("en-US", {
                      hour: "2-digit",
                      minute: "2-digit"
                    })} - ${new Date(flight.arrivalTime).toLocaleString("en-US", {
                      hour: "2-digit",
                      minute: "2-digit"
                    })}`}
                    duration={`${Math.floor(
                      (new Date(flight.arrivalTime).getTime() -
                        new Date(flight.departureTime).getTime()) /
                        (1000 * 60 * 60)
                    )}h ${Math.floor(
                      ((new Date(flight.arrivalTime).getTime() -
                        new Date(flight.departureTime).getTime()) %
                        (1000 * 60 * 60)) /
                        (1000 * 60)
                    )}m`}
                    stop="Nonstop"
                    hnl={`${flight.availableSeats} seats available`}
                    price={`$${flight.price}`}
                    trip="round trip"
                  />
                </div>
              ))
            )}
          </div>
          <div className="w-full lg:mt-12">
            <img src={map} alt="map" className="w-full h-full object-cover" />
          </div>
        </div>

        {priceShown && (
         <PriceGraph/>
        )}

        {!priceShown && (
          <div className="mt-10 flex flex-col gap-10 justify-end items-start lg:items-end">
            <PriceDetails flight={selectedFlight} />
            <button
              type="button"
              onClick={() => {
                const token = localStorage.getItem("token") || sessionStorage.getItem("token");
                if (!token) {
                  setSignin(true);
                  setSignup(false);
                  return;
                }
                navigate("/passenger-info", {
                  state: {
                    flight: selectedFlight
                  }
                });
              }}
              className="mt-5 text-[#605DEC] border-2 border-[#605DEC] py-2 px-3 rounded hover:bg-[#605DEC] hover:text-white transition-all duration-200"
            >
              Save & Close
            </button>
          </div>
        )}
      </div>
    </>
  );
};

export default FlightChoose;
