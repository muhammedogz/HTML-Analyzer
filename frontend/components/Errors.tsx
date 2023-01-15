import { DoneOutlined } from '@mui/icons-material';
import { Fade, IconButton, Stack, Typography } from '@mui/material';
import AnalyzerPane from 'components/AnalyzerPane';
import { Dispatch, SetStateAction } from 'react';
import { HtmlAnalyzerResponseType } from 'src/fetchers/htmlAnalyzerFetchers';

type ErrorsProps = {
  htmlAnalyze: HtmlAnalyzerResponseType | null;
  setHtmlAnalyze: Dispatch<SetStateAction<HtmlAnalyzerResponseType | null>>;
};

const Errors = ({ htmlAnalyze, setHtmlAnalyze }: ErrorsProps) => {
  return (
    <Fade
      in={htmlAnalyze !== null && htmlAnalyze.errors.length > 0}
      timeout={{
        enter: 750,
        exit: 150,
      }}
    >
      <Stack
        sx={{
          position: 'relative',
        }}
      >
        <AnalyzerPane gap={1}>
          <Typography fontWeight={700} fontSize="20px">
            Errors
          </Typography>
          <Stack>
            <IconButton
              id="close-button"
              onClick={() => {
                setHtmlAnalyze(null);
              }}
              sx={{
                position: 'absolute',
                top: '0px',
                right: '0px',
              }}
            >
              <DoneOutlined />
            </IconButton>
          </Stack>
          {htmlAnalyze?.errors.map((error, index) => {
            return (
              <Typography key={index} color="red">
                {error.reason}
              </Typography>
            );
          })}
        </AnalyzerPane>
      </Stack>
    </Fade>
  );
};

export default Errors;
