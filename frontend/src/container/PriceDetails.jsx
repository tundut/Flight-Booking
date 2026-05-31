// eslint-disable-next-line react/prop-types
import { united } from "../assets/logo";

const PriceDetails = ({flight}) => {
  if (!flight) {
    return <div>Select a flight to see details</div>;
  }

  const subtotal = flight.price || 0;
  const taxRate = 0.2; // 20% tax
  const taxes = Math.round(subtotal * taxRate * 100) / 100;
  const total = Math.round((subtotal + taxes) * 100) / 100;

  return (
    <>
      <div className="flex flex-col items-start lg:items-end justify-start lg:justify-end gap-5 w-full h-full sm:w-[400px]">
        <div className=" w-full border-[1px] border-[#E9E8FC] rounded-lg  flex flex-col gap-2">
          <div className="flex items-start justify-between w-full p-3 ">
            <div className="flex items-start justify-start gap-2">
              <img
                src={united}
                alt={flight.flightNumber}
                className="w-6 h-6 sm:w-9 sm:h-9 object-contain"
              />
              <div className="flex flex-col items-start justify-start">
                <h1 className="text-[#27273F] font-normal text-sm sm:text-base">
                  {flight.flightNumber}
                </h1>
                <p className="text-[#7C8DB0] font-normal text-sm sm:text-base">
                  {flight.from} → {flight.to}
                </p>
              </div>
            </div>
            <div className="flex flex-col items-end gap-2">
              <p className="text-[#27273F] font-normal text-sm sm:text-base">
                {Math.floor(
                  (new Date(flight.arrivalTime).getTime() -
                    new Date(flight.departureTime).getTime()) /
                    (1000 * 60 * 60)
                )}h{" "}
                {Math.floor(
                  ((new Date(flight.arrivalTime).getTime() -
                    new Date(flight.departureTime).getTime()) %
                    (1000 * 60 * 60)) /
                    (1000 * 60)
                )}m
              </p>
              <p className="text-[#27273F] font-normal text-sm sm:text-base">
                {new Date(flight.departureTime).toLocaleString("en-US", {
                  hour: "2-digit",
                  minute: "2-digit"
                })} - {new Date(flight.arrivalTime).toLocaleString("en-US", {
                  hour: "2-digit",
                  minute: "2-digit"
                })}
              </p>
              <p className="text-[#7C8DB0] font-normal text-sm sm:text-base">
                {flight.availableSeats} seats available
              </p>
            </div>
          </div>
        </div>
        <div className="flex flex-col gap-3 p-3 w-[231px]">
          <div className="w-full flex items-center justify-between text-[#27273F] text-sm sm:text-base">
            <p>Subtotal</p>
            <p>${subtotal.toFixed(2)}</p>
          </div>
          <div className="w-full flex items-center justify-between text-[#27273F] text-sm sm:text-base">
            <p>Taxes and Fees</p>
            <p>${taxes.toFixed(2)}</p>
          </div>
          <div className="w-full flex items-center justify-between text-[#27273F] text-sm sm:text-base font-bold">
            <p>Total</p>
            <p>${total.toFixed(2)}</p>
          </div>
        </div>
      </div>
    </>
  );
};

export default PriceDetails;
