import CustomTooltip from "../CustomTooltip/CustomTooltip";
import styles from "./OutcomeMixChart.module.css";
import { ResponsiveContainer, PieChart, Tooltip, Pie } from "recharts";
type Props = {
  data: { name: string; percentage: number; fill: string }[];
};
export default function OutcomeMixChart({ data }: Props) {
  return (
    <div className={styles.chartContainer}>
      <ResponsiveContainer width="100%" height="100%">
        <PieChart>
          <Pie
            data={data}
            dataKey="percentage"
            nameKey="name"
            innerRadius="55%"
            outerRadius="80%"
          ></Pie>
          <Tooltip
            content={<CustomTooltip />}
            cursor={{
              stroke: "#7b67ea",
              strokeDasharray: "4 4",
              strokeOpacity: 0.3,
            }}
          />
        </PieChart>
      </ResponsiveContainer>
    </div>
  );
}
