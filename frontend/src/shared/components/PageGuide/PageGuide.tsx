import StoryStep from "../StoryStep/StoryStep";

import styles from "./PageGuide.module.css";
type storyStepProps = {
  eyebrow: string;
  title: string;
  description: string;
};
type PageGuideProps = {
  howToReadTitle: string;
  howToReadDescription: string;
  storySteps: storyStepProps[];
};

export default function PageGuide(props: PageGuideProps) {
  return (
    <div className={styles.pageGuide}>
      <div className={styles.howTo}>
        <p className={styles.howToReadEyebrow}>How to read this page</p>
        <h3 className={styles.howToReadTitle}>{props.howToReadTitle}</h3>
        <p className={styles.howToReadDescription}>
          {props.howToReadDescription}
        </p>
      </div>
      {/*+++++++++++++++++++++++++++++To be created as different component +++++++++++++++++++++++++++++++++++++++++++++++*/}
      {props.storySteps.map((step) => (
        <StoryStep
          key={step.title}
          eyebrow={step.eyebrow}
          title={step.title}
          description={step.description}
        />
      ))}
    </div>
  );
}
