interface InputFieldProps {
  label: string;
  type: 'email' | 'password' | 'text';
  value: string;
  placeholder: string;
  onChange: (value: string) => void;
}

export function InputField({ label, type, value, placeholder, onChange }: InputFieldProps) {
  return (
    <div>
      <label className="block text-xs font-bold uppercase tracking-widest text-slate-500 mb-2">
        {label}
      </label>
      <input
        type={type}
        value={value}
        onChange={(e) => onChange(e.target.value)}
        required
        placeholder={placeholder}
        className="w-full px-4 py-3 rounded-xl bg-slate-950 border border-slate-800 text-slate-300 focus:border-blue-600 focus:outline-none focus:ring-1 focus:ring-blue-600 transition font-mono text-sm"
      />
    </div>
  );
}