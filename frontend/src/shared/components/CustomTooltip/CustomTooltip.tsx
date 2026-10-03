import { formatCompactNumber } from "../../../Utils/formatters";
import styles from "./CustomToolTip.module.css";

type TooltipData = {
  name?: string;
  value?: number;
  percentage?: number;
};

type CustomTooltipProps = {
  active?: boolean;
  payload?: Array<{
    value?: number;
    payload: TooltipData;
  }>;
  percentageLabel?: string;
  valueLabel?: string;
};

export default function CustomTooltip({
  active,
  payload,
  percentageLabel = "Percentage",
  valueLabel = "Number of Flights",
}: CustomTooltipProps) {
  if (!active || !payload?.length) {
    return null;
  }

  const data = payload[0].payload;

  return (
    <div className={styles.Tooltip}>
      {data.name && <p className={styles.TooltipName}>{data.name}</p>}

      {data.percentage !== undefined && (
        <p className={styles.TooltipValue}>
          {percentageLabel}: <span>{data.percentage.toFixed(2)}%</span>
        </p>
      )}

      {data.value !== undefined && (
        <p className={styles.TooltipValue}>
          {valueLabel}:{" "}
          <span>
            {typeof data.value === "number"
              ? formatCompactNumber(data.value)
              : data.value}
          </span>
        </p>
      )}
    </div>
  );
}
