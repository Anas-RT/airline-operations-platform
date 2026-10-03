import styles from "./Dropdown.module.css";

type Props = {
  label: string;
  options: (string | number)[];
  value: string | number;
  onChange: (value: string) => void;
};

export default function Dropdown({ label, options, value, onChange }: Props) {
  return (
    <div className={styles.dropdownContainer}>
      <p>{label}</p>

      <select value={value} onChange={(event) => onChange(event.target.value)}>
        {options.map((option) => (
          <option key={option} value={option}>
            {option}
          </option>
        ))}
      </select>
    </div>
  );
}
