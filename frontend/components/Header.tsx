import { Stack, Typography } from "@mui/material";

const Header = () => {
  return (
    <Stack textAlign="center" color="#fff">
      <Typography fontSize="28px" fontWeight={500}>
        Welcome To The Perfect HTML Analyzer App
      </Typography>
      <Typography fontSize="22px">
        You can enter HTML code in the editor below and see the output
      </Typography>
    </Stack>
  );
};

export default Header;
