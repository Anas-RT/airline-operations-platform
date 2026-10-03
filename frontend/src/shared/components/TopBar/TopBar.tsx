import styles from "./TopBar.module.css";

export default function TopBar({
  title,
  description,
}: {
  title: string;
  description: string;
}) {
  return (
    <div className={styles.topBar}>
      <h1>{title}</h1>
      <p>{description}</p>
    </div>
  );
}
