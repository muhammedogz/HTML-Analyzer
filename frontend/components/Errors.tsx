import { Button, Fade, Stack, Tooltip, Typography } from '@mui/material';
import AnalyzerPane from 'components/AnalyzerPane';
import { Dispatch, SetStateAction, useRef } from 'react';
import Pdf from 'react-to-pdf';
import { ErrorLevelEnums, HtmlAnalyzerResponseType } from 'src/fetchers/htmlAnalyzerFetchers';

type ErrorsProps = {
  htmlAnalyze: HtmlAnalyzerResponseType | null;
  setHtmlAnalyze: Dispatch<SetStateAction<HtmlAnalyzerResponseType | null>>;
};

const Errors = ({ htmlAnalyze, setHtmlAnalyze }: ErrorsProps) => {
  // give emojis to the errors
  // warning: ⚠️
  // error: ❌
  // SEO: 📈
  // Accessibility: 🦾

  // give color values in hex to the errors
  // warning: #FFC107
  // error: #F44336
  // SEO: #c81ea9
  // Accessibility: #2196F3

  // give color value for solution text
  // solution: #4CAF50

  // give emoji for solution text
  // solution: ✅

  const ref = useRef();

  return (
    <Fade
      in={htmlAnalyze !== null && htmlAnalyze.errors.length > 0}
      timeout={{
        enter: 750,
        exit: 150,
      }}
    >
      <Stack gap="2px">
        <Stack
          gap={3}
          ref={ref}
          sx={{
            position: 'relative',
          }}
        >
          <Tooltip title="Score calculated based on the number of errors and warnings">
            <Stack>
              <AnalyzerPane>
                <Typography fontWeight={700} fontSize="20px" textAlign="center">
                  Your HTML Score
                </Typography>
                <Typography fontWeight={700} fontSize="50px" textAlign="center">
                  {htmlAnalyze?.rate}
                </Typography>
              </AnalyzerPane>
            </Stack>
          </Tooltip>
          <AnalyzerPane gap={1}>
            <Typography fontWeight={700} fontSize="20px">
              Errors - {htmlAnalyze?.errors.length}
            </Typography>
            <Stack flexDirection="row" gap="10px">
              <Typography fontWeight={700} fontSize="12px">
                Meanings:
              </Typography>
              <Typography fontWeight={700} fontSize="12px" color="#F44336">
                ❌ Error
              </Typography>
              <Typography fontWeight={700} fontSize="12px" color="#FFC107">
                ⚠️ Warning
              </Typography>
              <Typography fontWeight={700} fontSize="12px" color="#c81ea9">
                📈 SEO
              </Typography>
              <Typography fontWeight={700} fontSize="12px" color="#2196F3">
                🦾 Accessibility
              </Typography>
              <Typography fontWeight={700} fontSize="12px" color="#4CAF50">
                ✅ Solution
              </Typography>
            </Stack>
            {htmlAnalyze?.errors.map((error, index) => {
              return (
                <Stack key={index}>
                  <Typography
                    color={
                      error.errorLevel === ErrorLevelEnums.ERROR
                        ? '#F44336'
                        : error.errorLevel === ErrorLevelEnums.WARNING
                        ? '#FFC107'
                        : error.errorLevel === ErrorLevelEnums.SEO
                        ? '#c81ea9'
                        : '#2196F3'
                    }
                  >
                    {error.errorLevel === ErrorLevelEnums.ERROR ? (
                      <Tooltip title="Error" placement="left">
                        <span>❌</span>
                      </Tooltip>
                    ) : error.errorLevel === ErrorLevelEnums.WARNING ? (
                      <Tooltip title="Warning" placement="left">
                        <span>⚠️</span>
                      </Tooltip>
                    ) : error.errorLevel === ErrorLevelEnums.SEO ? (
                      <Tooltip title="SEO" placement="left">
                        <span>📈</span>
                      </Tooltip>
                    ) : (
                      <Tooltip title="Accessibility" placement="left">
                        <span>🦾</span>
                      </Tooltip>
                    )}
                    {error.reason}
                  </Typography>
                  <Typography color="#224d24">
                    <Tooltip title="Solution" placement="left">
                      <span>✅</span>
                    </Tooltip>{' '}
                    {error.solution}
                  </Typography>
                </Stack>
              );
            })}
          </AnalyzerPane>
        </Stack>
        <Pdf targetRef={ref} filename="analyze-report.pdf">
          {({ toPdf }: { toPdf: any }) => (
            <Button
              sx={{
                background: '#4CAF50 !important',
                color: '#fff !important',
                borderRadius: '20px !important',
                '&:hover': {
                  background: '#4caf4f69 !important',
                  color: '#fff !important',
                },
              }}
              onClick={toPdf}
            >
              Generate PDF Report
            </Button>
          )}
        </Pdf>
      </Stack>
    </Fade>
  );
};

export default Errors;
