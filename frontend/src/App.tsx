import '@fontsource/roboto/300.css';
import '@fontsource/roboto/400.css';
import '@fontsource/roboto/500.css';
import '@fontsource/roboto/700.css';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import { Accordion, AccordionDetails, AccordionSummary, Tab, Tabs, Typography } from '@mui/material';
import { Stack } from '@mui/system';
import 'ace-builds/src-noconflict/ace';
import AnalyzerPane from 'components/AnalyzerPane';
import Header from 'components/Header';
import InputMode from 'components/InputMode';
import URLMode from 'components/URLMode';
import { useState } from 'react';
import QRCode from 'react-qr-code';
import SwipeableViews from 'react-swipeable-views';

function App() {
  const [tabValue, setTabValue] = useState(0);

  return (
    <Stack
      alignItems="center"
      sx={{
        minHeight: '100vh',
        background: 'linear-gradient(rgba(196, 102, 0, 0.6), rgba(155, 89, 182, 0.6))',
      }}
      p={{ xs: 2, md: 3 }}
      gap={8}
    >
      <Header />

      <Stack gap={3} alignItems="center">
        <Accordion>
          <AccordionSummary expandIcon={<ExpandMoreIcon />} aria-controls="panel1a-content" id="panel1a-header">
            <Typography>Show qr code</Typography>
          </AccordionSummary>
          <AccordionDetails>
            <Stack justifyContent="center" alignItems="center">
              <Typography textAlign="center">Read QR Code to Open HTML Analyzer!</Typography>
              <Stack
                sx={{
                  backgroundColor: 'white',
                  borderRadius: '24px',
                }}
              >
                <QRCode value="https://muhammedogz.github.io/HTML-Analyzer/" />
              </Stack>
            </Stack>
          </AccordionDetails>
        </Accordion>
        <Stack>
          <Typography color="#fff" fontSize="17px" fontWeight={500} textAlign="center">
            Chose your input method
          </Typography>
          <AnalyzerPane>
            <Tabs
              value={tabValue}
              onChange={(_event, value) => {
                setTabValue(value);
              }}
            >
              <Tab label="Input Mode" />
              <Tab label="URL Mode" />
            </Tabs>
          </AnalyzerPane>
        </Stack>
        <Stack>
          <SwipeableViews
            axis="x"
            index={tabValue}
            onChangeIndex={(index) => {
              setTabValue(index);
            }}
          >
            <Stack
              id="html-mode-stack"
              key={0}
              sx={{
                display: tabValue === 0 ? 'block' : 'none',
              }}
            >
              <InputMode />
            </Stack>
            <Stack
              id="url-mode-stack"
              key={1}
              sx={{
                display: tabValue === 1 ? 'block' : 'none',
              }}
            >
              <URLMode />
            </Stack>
          </SwipeableViews>
        </Stack>
      </Stack>
    </Stack>
  );
}
export default App;
