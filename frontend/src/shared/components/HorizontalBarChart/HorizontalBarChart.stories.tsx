import type { Meta, StoryObj } from "@storybook/react-vite";

import HorizontalBarChart from "./HorizontalBarChart";

const meta = {
  title: "Charts/HorizontalBarChart",
  component: HorizontalBarChart,
} satisfies Meta<typeof HorizontalBarChart>;

export default meta;

type Story = StoryObj<typeof meta>;

export const Default: Story = {
  args: {
    data: [
      { name: "Hawaiian", percentage: 84.7 },
      { name: "Alaska", percentage: 82.5 },
      { name: "Delta", percentage: 81.6 },
      { name: "Southwest", percentage: 79.8 },
      { name: "American", percentage: 77.9 },
      { name: "United", percentage: 76.8 },
      { name: "JetBlue", percentage: 72.4 },
    ],
  },
};
