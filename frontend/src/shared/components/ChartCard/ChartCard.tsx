import styles from "./ChartCard.module.css";

type ChartCardProps = {
  title: string;
  description: string;
  children: React.ReactNode;
};

export default function ChartCard({
  title,
  description,
  children,
}: ChartCardProps) {
  return (
    <div className={styles.chartCard}>
      <div className={styles.header}>
        <h3>{title}</h3>
        <p>{description}</p>
      </div>
      {children}
    </div>
  );
}
