import styles from "./HorizontalBarChart.module.css";
import CustomTooltip from "../CustomTooltip/CustomTooltip";
import {
  ResponsiveContainer,
  BarChart,
  Bar,
  XAxis,
  YAxis,
  Tooltip,
  Rectangle,
  LabelList,
} from "recharts";

type Props = {
  data: { name: string; percentage: number }[];
};

export default function HorizontalBarChart({ data }: Props) {
  const getBarFill = (percentage: number, index: number) => {
    const high = ["#2FAE83", "#45B994", "#65C9AA"];
    const good = ["#7565E8", "#8878EA", "#9A8DEC", "#6F63DD"];
    const mid = ["#A294E9", "#9185DF", "#B0A4EE"];
    const low = ["#FF8F91", "#FF7378", "#F58A7A", "#EB6F76"];

    if (percentage >= 82) return high[index % high.length];
    if (percentage >= 78) return good[index % good.length];
    if (percentage >= 75) return mid[index % mid.length];

    return low[index % low.length];
  };

  return (
    <div
      className={styles.chartContainer}
      style={
        data.length >= 8 ? { height: `${data.length * 1.4}rem` } : undefined
      }
    >
      <ResponsiveContainer width="100%" height="100%">
        <BarChart data={data} layout="vertical" margin={{ right: 20 }}>
          <XAxis type="number" domain={[0, 100]} hide />

          <YAxis
            type="category"
            dataKey="name"
            axisLine={false}
            tickLine={false}
            width={120}
            tick={{
              fill: "#1f2233",
              fontSize: "0.5rem",
              fontWeight: 700,
            }}
          />

          <Tooltip
            content={<CustomTooltip />}
            cursor={{
              stroke: "#7b67ea",
              strokeDasharray: "4 4",
              strokeOpacity: 0.3,
            }}
          />

          <Bar
            dataKey="percentage"
            barSize={data.length >= 8 ? 14 : 25}
            background={(props) => (
              <Rectangle {...props} fill="#ECEAF3" radius={10} />
            )}
            shape={(props) => (
              <Rectangle
                {...props}
                fill={getBarFill(Number(props.value), props.index)}
                radius={10}
              />
            )}
          >
            <LabelList
              dataKey="percentage"
              position="right"
              formatter={(percentage) => `${Number(percentage).toFixed(1)}%`}
              style={{
                fill: "#1f2233",
                fontSize: "0.5rem",
                fontWeight: 700,
              }}
            />
          </Bar>
        </BarChart>
      </ResponsiveContainer>
    </div>
  );
}
