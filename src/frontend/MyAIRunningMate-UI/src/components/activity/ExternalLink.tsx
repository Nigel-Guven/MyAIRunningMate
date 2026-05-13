interface Props {
  label: string;
  href?: string;
}

export const ExternalLink = ({ label, href }: Props) => {
  return (
    <a
      href={href}
      target="_blank"
      rel="noopener noreferrer"
      className="
        flex items-center justify-between
        px-4 py-3
        rounded-lg
        bg-slate-800 hover:bg-slate-700
        border border-slate-700
        transition
        group
      "
    >
      <span className="text-slate-200 text-sm font-medium">
        {label}
      </span>

      <span className="text-slate-500 text-xs group-hover:text-slate-300">
        Open →
      </span>
    </a>
  );
}