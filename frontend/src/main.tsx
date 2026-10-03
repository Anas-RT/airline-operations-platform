import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import "@fontsource-variable/inter/wght.css";
import "./index.css";
import App from "./app/App.tsx";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import {
  AirlinePerformancePage,
  DelaySeverityPage,
  NetworkOverviewPage,
} from "./pages/index.ts";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    children: [
      {
        path: "/network-overview",
        element: <NetworkOverviewPage />,
      },
      { index: true, path: "/airlines", element: <AirlinePerformancePage /> },
      {
        path: "/delay-severity",
        element: <DelaySeverityPage />,
      },
    ],
  },
]);
createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <RouterProvider router={router} />
  </StrictMode>,
);
