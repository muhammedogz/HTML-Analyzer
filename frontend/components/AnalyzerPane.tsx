import { Typography } from "@mui/material";
import { Stack } from "@mui/system";
import Header from "components/Header";
import HTMLEditor from "components/HTMLEditor";

interface IAnalyzerPaneProps {
  children: React.ReactNode;
}

const AnalyzerPane = ({ children }: IAnalyzerPaneProps) => {
  return (
    <Stack
      sx={{
        background: "white",
        borderRadius: "20px",
        boxShadow: "0 0 15px 1px rgba(0, 0, 0, 0.4)",
        padding: "20px 30px",
      }}
    >
      {children}
    </Stack>
  );
};

export default AnalyzerPane;
