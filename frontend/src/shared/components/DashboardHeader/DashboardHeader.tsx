import styles from "./DashboardHeader.module.css";

export default function DashboardHeader({
  eyebrow,
  title,
  description,
  tags,
}: {
  eyebrow: string;
  title: string;
  description: string;
  tags: string[];
}) {
  return (
    <>
      <div className={styles.dashboardHeader}>
        <div className={styles.headerContent}>
          <h4>{eyebrow}</h4>
          <h1>{title}</h1>
          <p>{description}</p>
        </div>
        <div className={styles.headerTags}>
          {tags.map((tag) => (
            <span key={tag} className={styles.headerTag}>
              {tag}
            </span>
          ))}
        </div>
      </div>
    </>
  );
}
