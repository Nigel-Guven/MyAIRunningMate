interface Props {
  label: string;
  id?: string;
}

export const ExternalLink = ({
  label,
  id,
}: Props) => {

  return (
    <div className="flex justify-between items-center text-xs">

      <span className="text-slate-500 uppercase font-bold">
        {label}
      </span>

      <span className="font-mono text-slate-300">
        {id || 'None'}
      </span>

    </div>
  );
};