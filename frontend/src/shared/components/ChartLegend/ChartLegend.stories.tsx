// ChartLegend.stories.tsx

import type { Meta, StoryObj } from "@storybook/react";
import ChartLegend from "./ChartLegend";

const meta = {
  component: ChartLegend,
} satisfies Meta<typeof ChartLegend>;

export default meta;

type Story = StoryObj<typeof meta>;

export const Default: Story = {
  args: {
    data: [{
      "name": "OTP15",
      "percentage": 66,
      "fill": "#756999"
    }, {
      "name": "Moderate delay",
      "percentage": 12.1,
      "fill": "#B8AEF3"
    }, {
      "name": "Severe delay",
      "percentage": 5.8,
      "fill": "#F2A65A"
    }, {
      "name": "Cancelled / diverted",
      "percentage": 1.9,
      "fill": "#FF6B6B"
    }],
  },
};
