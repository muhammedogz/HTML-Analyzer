import { Stack, StackProps } from '@mui/material';

type IAnalyzerPaneProps = StackProps & {
  children: React.ReactNode;
};

const AnalyzerPane = ({ children, ...rest }: IAnalyzerPaneProps) => {
  return (
    <Stack
      sx={{
        background: 'white',
        borderRadius: '20px',
        padding: '6px 12px',
      }}
      {...rest}
    >
      {children}
    </Stack>
  );
};

export default AnalyzerPane;
