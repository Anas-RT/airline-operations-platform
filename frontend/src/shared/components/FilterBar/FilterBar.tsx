import Dropdown from "../Dropdown/Dropdown";
import { useState } from "react";
import styles from "./FilterBar.module.css";
import { formatName } from "../../../Utils/formatters";
import type {
  NetworkOverviewFilters,
  NetworkOverviewFilterState,
} from "../../types/NetworkOverviewTypes";

type FilterOptions = {
  year: string[];
  airline: string[];
  month: string[];
  timeBand: string[];
};

type Props = {
  filterOptions: FilterOptions;
  onApply: (filters: NetworkOverviewFilterState) => void;
};

export default function FilterBar({ filterOptions, onApply }: Props) {
  const [filters, setFilters] = useState<NetworkOverviewFilterState>({
    year: "All",
    airline: "All",
    month: "All",
    timeBand: "All",
  });

  const handleChange = (
    key: keyof NetworkOverviewFilterState,
    value: string | number,
  ) => {
    setFilters((current) => ({
      ...current,
      [key]: String(value),
    }));
  };

  const handleReset = () => {
    const resetFilters = {
      year: "All",
      airline: "All",
      month: "All",
      timeBand: "All",
    };

    setFilters(resetFilters);
    onApply(resetFilters);
  };

  return (
    <div className={styles.filterBar}>
      <div className={styles.dropdownContainer}>
        {(
          Object.entries(filterOptions) as [
            keyof NetworkOverviewFilters,
            (string | number)[],
          ][]
        ).map(([key, options]) => (
          <Dropdown
            key={key}
            label={formatName(key)}
            options={["All", ...options]}
            value={filters[key]}
            onChange={(value) => handleChange(key, value)}
          />
        ))}
      </div>

      <div className={styles.buttonsContainer}>
        <button className={styles.applyButton} onClick={() => onApply(filters)}>
          Apply
        </button>

        <button className={styles.resetButton} onClick={handleReset}>
          Reset
        </button>
      </div>
    </div>
  );
}
