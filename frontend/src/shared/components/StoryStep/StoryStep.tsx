import styles from "./StoryStep.module.css";

type storyStepProps = {
  eyebrow: string;
  title: string;
  description: string;
};

export default function StoryStep(props: storyStepProps) {
  return (
    <div className={styles.storyStep}>
      <p className={styles.eyebrow}>{props.eyebrow}</p>
      <h3 className={styles.title}>{props.title}</h3>
      <p className={styles.description}>{props.description}</p>
    </div>
  );
}
