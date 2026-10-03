import styles from "./PaginatedTable.module.css";
import type { AirlineScorecardRow } from "../../types/AirlinePerformanceTypes";

type Props = {
  data: AirlineScorecardRow[];
  columns: string[];
  pageSize: number;
  pageNumber: number;
  totalCount: number;
  onPageChange: (page: number) => void;
};

export default function PaginatedTable({
  data,
  columns,
  pageSize,
  pageNumber,
  totalCount,
  onPageChange,
}: Props) {
  const numberOfPages = Math.ceil(totalCount / pageSize);
  const pages = Array.from({ length: numberOfPages }, (_, i) => i + 1);
  return (
    <div className={styles.paginatedTable}>
      <div className={styles.header}>
        <h3>All-airline scorecard </h3>
        <p>Overview of airline performance metrics.</p>
      </div>
      <table>
        <thead>
          <tr>
            {columns.map((column) => (
              <th key={column}>{column}</th>
            ))}
          </tr>
        </thead>

        <tbody>
          {data.map((row) => (
            <tr key={row.airlineCode}>
              <td>
                <span>{row.airlineCode}</span>
                {row.airlineName}
              </td>
              <td>{row.completedFlights}</td>
              <td>{row.otp15Rate}%</td>
              <td>{row.severeDelayRate}%</td>
              <td>{row.severeCases}</td>
              <td>{row.cancellationRate}%</td>
              <td>{row.priorityInterpretation}</td>
            </tr>
          ))}
        </tbody>
      </table>
      <div className={styles.footer}>
        <div className={styles.currentPageInfo}>
          <p>
            Showing {pageSize} of {totalCount} airlines
          </p>
        </div>
        <div className={styles.paginationControls}>
          <button
            onClick={() => onPageChange(pageNumber - 1)}
            disabled={pageNumber === 1}
          >
            &larr;
          </button>
          {pages.map((page) => (
            <span
              className={page == pageNumber ? styles.activePage : ""}
              key={page}
            >
              {page}
            </span>
          ))}
          <button
            onClick={() => onPageChange(pageNumber + 1)}
            disabled={pageNumber === numberOfPages}
          >
            &rarr;
          </button>
        </div>
      </div>
    </div>
  );
}
