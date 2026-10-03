import styles from "./NarrativeCard.module.css";

type Props = {
  analystQuestion?: string;
  eyebrow?: string;
  attention?: boolean;
  title: string;
  description: string;
};

export default function NarrativeCard({
  eyebrow,
  attention,
  title,
  description,
}: Props) {
  return (
    <div className={`${styles.card} ${attention ? styles.attention : ""}`}>
      {eyebrow && <p className={styles.eyebrow}>{eyebrow}</p>}
      <h3 className={styles.title}>{title}</h3>
      <p className={styles.description}>{description}</p>
    </div>
  );
}
