interface CustomCountyFlagProps {
  primaryColor: string;
  secondaryColor: string;
  layout?: 'vertical' | 'horizontal' | 'bar';
  className?: string;
}

export const CountyFlag = ({ 
  primaryColor, 
  secondaryColor, 
  layout = 'horizontal',
  className = "w-6 h-4" 
}: CustomCountyFlagProps) => {
  return (
    <svg 
      viewBox="0 0 60 40" 
      preserveAspectRatio="xMidYMid slice" 
      className={`${className} flex-shrink-0 border border-slate-800/60 shadow-sm`} 
    >
      {layout === 'horizontal' && (
        <>
          <rect width="60" height="20" fill={primaryColor} />
          <rect y="20" width="60" height="20" fill={secondaryColor} />
        </>
      )}

      {layout === 'vertical' && (
        <>
          <rect width="30" height="40" fill={primaryColor} />
          <rect x="30" width="30" height="40" fill={secondaryColor} />
        </>
      )}

      {layout === 'bar' && (
        <>
          <rect width="60" height="40" fill={primaryColor} />
          <rect y="13" width="60" height="14" fill={secondaryColor} />
        </>
      )}
    </svg>
  );
};