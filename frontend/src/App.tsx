import { Typography } from "@mui/material";
import { Stack } from "@mui/system";
import AnalyzerPane from "components/AnalyzerPane";
import Header from "components/Header";
import HTMLEditor from "components/HTMLEditor";

function App() {
  return (
    <Stack
      alignItems="center"
      sx={{
        height: "100vh",
        background:
          "linear-gradient(rgba(196, 102, 0, 0.6), rgba(155, 89, 182, 0.6))",
      }}
      p={8}
      gap={8}
    >
      <Header />
      <AnalyzerPane>
        <HTMLEditor />
      </AnalyzerPane>
    </Stack>
  );
}
export default App;
