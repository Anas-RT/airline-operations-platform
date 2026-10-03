import styles from "./MonthlyTrendCard.module.css";
import CustomTooltip from "../CustomTooltip/CustomTooltip";
import {
  ResponsiveContainer,
  AreaChart,
  Area,
  CartesianGrid,
  XAxis,
  YAxis,
  Tooltip,
  ReferenceDot,
  Legend,
} from "recharts";

type MonthlyTrend = {
  month: string;
  otp15Pct: number;
  comparisonOtp15Pct?: number;
};

type Props = {
  data: MonthlyTrend[];
  primaryLineName?: string;
  comparisonLineName?: string;
  primaryArea?: boolean;
  comparisonArea?: boolean;
  primaryDot?: boolean;
  comparisonDot?: boolean;
};

export default function MonthlyTrendChart({
  data,
  primaryLineName = "OTP15 %",
  comparisonLineName = "Network OTP15 %",
  primaryArea = true,
  comparisonArea = false,
  primaryDot = true,
  comparisonDot = false,
}: Props) {
  if (!data.length) {
    return null;
  }

  const lowestPoint = data.reduce((lowest, current) =>
    current.otp15Pct < lowest.otp15Pct ? current : lowest,
  );

  const highestPoint = data.reduce((highest, current) =>
    current.otp15Pct > highest.otp15Pct ? current : highest,
  );

  const hasComparisonLine = data.some(
    (item) => item.comparisonOtp15Pct !== undefined,
  );

  return (
    <div className={styles.chartContainer}>
      <ResponsiveContainer width="100%" height="100%">
        <AreaChart
          data={data}
          margin={{
            top: 30,
            right: 30,
            bottom: 10,
            left: 10,
          }}
        >
          <ReferenceDot
            x={lowestPoint.month}
            y={lowestPoint.otp15Pct}
            r={9}
            fill="#ff6b6b"
            stroke="#ffffff"
            strokeWidth={4}
          />

          <ReferenceDot
            x={highestPoint.month}
            y={highestPoint.otp15Pct}
            r={9}
            fill="#2eae82"
            stroke="#ffffff"
            strokeWidth={4}
          />

          <defs>
            <linearGradient id="otp15Gradient" x1="0" y1="0" x2="0" y2="1">
              <stop offset="0%" stopColor="#7b67ea" stopOpacity={0.22} />
              <stop offset="100%" stopColor="#7b67ea" stopOpacity={0} />
            </linearGradient>

            <linearGradient id="comparisonGradient" x1="0" y1="0" x2="0" y2="1">
              <stop offset="0%" stopColor="#FF7378" stopOpacity={0.16} />
              <stop offset="100%" stopColor="#FF7378" stopOpacity={0} />
            </linearGradient>
          </defs>

          <CartesianGrid
            vertical={false}
            stroke="#e7e4ef"
            strokeDasharray="5 7"
          />

          <XAxis
            dataKey="month"
            interval={0}
            tickFormatter={(month) => month.slice(0, 3)}
            axisLine={false}
            tickLine={false}
            tick={{
              fill: "#858591",
              fontSize: 13,
            }}
            padding={{
              left: 10,
              right: 10,
            }}
          />

          <YAxis
            domain={[65, 85]}
            ticks={[70, 75, 80, 85]}
            axisLine={false}
            tickLine={false}
            tick={{
              fill: "#858591",
              fontSize: 13,
            }}
            tickFormatter={(value) => `${value}%`}
          />

          <Tooltip
            content={<CustomTooltip />}
            cursor={{
              stroke: "#7b67ea",
              strokeDasharray: "4 4",
              strokeOpacity: 0.3,
            }}
          />

          <Area
            dataKey="otp15Pct"
            stroke="#7b67ea"
            strokeWidth={3}
            name={primaryLineName}
            fill={primaryArea ? "url(#otp15Gradient)" : "transparent"}
            dot={
              primaryDot
                ? {
                    r: 4,
                    fill: "#ffffff",
                    stroke: "#7b67ea",
                    strokeWidth: 2,
                  }
                : false
            }
            activeDot={{
              r: 8,
              fill: "#7b67ea",
              stroke: "#ffffff",
              strokeWidth: 3,
            }}
          />

          {hasComparisonLine && (
            <Area
              dataKey="comparisonOtp15Pct"
              stroke="#FF7378"
              strokeWidth={2}
              name={comparisonLineName}
              fill={comparisonArea ? "url(#comparisonGradient)" : "transparent"}
              dot={
                comparisonDot
                  ? {
                      r: 4,
                      fill: "#ffffff",
                      stroke: "#FF7378",
                      strokeWidth: 2,
                    }
                  : false
              }
            />
          )}

          <Legend
            formatter={(value) => (
              <span style={{ fontWeight: "800" }}>{value}</span>
            )}
          />
        </AreaChart>
      </ResponsiveContainer>
    </div>
  );
}
