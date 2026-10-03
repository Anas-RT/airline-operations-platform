export const formatName = (key: string) =>
  key
    .replace(/Flights$/, "")
    .replace(/([A-Z])/g, " $1")
    .replace(/^./, (char) => char.toUpperCase())
    .replace("Otp15", "OTP15");

export const formatCompactNumber = (value: number) => {
  if (value >= 1_000_000) {
    return `${new Intl.NumberFormat("en", {
      maximumFractionDigits: 1,
    }).format(value / 1_000_000)}M`;
  }

  if (value >= 1_000) {
    return `${new Intl.NumberFormat("en", {
      maximumFractionDigits: 1,
    }).format(value / 1_000)}K`;
  }

  return value.toString();
};
