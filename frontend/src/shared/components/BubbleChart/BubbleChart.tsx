import {
  ScatterChart,
  Scatter,
  XAxis,
  YAxis,
  ZAxis,
  CartesianGrid,
  Tooltip,
  ResponsiveContainer,
} from "recharts";
import CustomTooltip from "../CustomTooltip/CustomTooltip";

type BubbleChartProps = {
  range?: [number, number];
  data: { x: number; y: number; z: number }[];
};

export default function BubbleChart(props: BubbleChartProps) {
  return (
    <ResponsiveContainer width="100%" height="100%">
      <ScatterChart>
        <CartesianGrid stroke="#e7e4ef" />
        <XAxis
          type="number"
          dataKey="x"
          name="X"
          tickLine={false}
          tickFormatter={() => ""}
          label={{
            value: "OTP15 Eligible Flights",
            position: "insideBottom",
            fontSize: 9,
            fontWeight: 600,
            offset: 15,
          }}
        />
        <YAxis
          type="number"
          dataKey="y"
          name="Y"
          tickLine={false}
          tickFormatter={() => ""}
          label={{
            value: "Severe-delay rate",
            angle: -90,
            position: "center",
            fontSize: 9,
            fontWeight: 600,
          }}
        />
        <ZAxis
          type="number"
          dataKey="z"
          range={props.range ?? [60, 100]}
          name="Z"
        />
        <Tooltip content={<CustomTooltip />} />
        <Scatter
          name="Bubbles"
          data={props.data}
          fill="#8884d8"
          stroke="#e7e4ef"
          strokeWidth={4}
        />
      </ScatterChart>
    </ResponsiveContainer>
  );
}
