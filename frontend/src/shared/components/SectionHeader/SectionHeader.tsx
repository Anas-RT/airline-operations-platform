import styles from "./SectionHeader.module.css";

type sectionHeaderProps = {
  title: string;
  description: string;
};

export default function SectionHeader(props: sectionHeaderProps) {
  return (
    <div className={styles.sectionHeader}>
      <h2>{props.title}</h2>
      <p>{props.description}</p>
    </div>
  );
}
