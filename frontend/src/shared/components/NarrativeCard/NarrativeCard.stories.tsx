import type { Meta, StoryObj } from "@storybook/react-vite";

import NarrativeCard from "./NarrativeCard";

const meta = {
  title: "Components/NarrativeCard",
  component: NarrativeCard,
} satisfies Meta<typeof NarrativeCard>;

export default meta;

type Story = StoryObj<typeof meta>;

export const Default: Story = {
  args: {
    eyebrow: "Airline Performance",
    title: "Customer Satisfaction Scores",
    description:
      "Hawaiian Airlines leads in customer satisfaction, followed by Alaska and Delta.",
  },
};
