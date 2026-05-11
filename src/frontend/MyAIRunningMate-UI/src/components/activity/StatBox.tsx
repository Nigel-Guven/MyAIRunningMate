interface Props {
  label: string;
  value: string;
}

export const StatBox = ({
  label,
  value,
}: Props) => {

  return (
    <div className="bg-slate-900 border border-slate-800 p-6 rounded-xl">

      <p className="text-slate-500 text-[10px] font-black uppercase tracking-widest">
        {label}
      </p>

      <p className="text-2xl font-bold mt-1">
        {value}
      </p>

    </div>
  );
};