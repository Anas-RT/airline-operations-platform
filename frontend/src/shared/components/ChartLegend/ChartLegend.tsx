import styles from "./ChartLegend.module.css";

type LegendItem = {
  name: string;
  percentage: number;
  fill: string;
};

type Props = {
  data: LegendItem[];
};

export default function ChartLegend({ data }: Props) {
  return (
    <div className={styles.legendContainer}>
      {data.map((item) => (
        <div className={styles.legendItem} key={item.name}>
          <span
            className={styles.legendColor}
            style={{ backgroundColor: item.fill }}
          />

          <span>{item.name}:</span>
          <strong>{item.percentage.toFixed(1)}%</strong>
        </div>
      ))}
    </div>
  );
}
