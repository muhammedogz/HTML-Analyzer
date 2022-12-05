import LoadingButton from '@mui/lab/LoadingButton';
import { Fade, FormLabel, Stack, Typography } from '@mui/material';
import AnalyzerPane from 'components/AnalyzerPane';
import Editor from 'components/Editor';
import { useCallback, useState } from 'react';
import { getAnalyzeFromHtml, HTMlAnalyzerType } from 'src/fetchers/htmlAnalyzerFetchers';

function InputMode() {
  const [code, setCode] = useState('<h1>Hello World!</h1>');
  const [htmlAnalyze, setHtmlAnalyze] = useState<HTMlAnalyzerType | null>(null);
  const [loading, setLoading] = useState(false);

  const sendHtml = useCallback(async () => {
    try {
      setLoading(true);
      const response = await getAnalyzeFromHtml(code);
      if (response.status === 200) {
        setHtmlAnalyze(response.data);
        setLoading(false);
      }
    } catch (error) {
      console.error(error);
    }
  }, [code]);

  return (
    <Stack gap={3} id="html-editor-stack" flexDirection="row">
      <Stack gap={3}>
        <AnalyzerPane gap={2}>
          <FormLabel htmlFor="html-editor">
            <Typography fontWeight={700} fontSize="20px" pl="10px">
              Enter HTML Input
            </Typography>
          </FormLabel>
          <Editor code={code} setCode={setCode} />
        </AnalyzerPane>
        <AnalyzerPane justifyContent="center" alignItems="center">
          <LoadingButton
            loading={loading}
            color="secondary"
            variant="contained"
            onClick={sendHtml}
            sx={{
              width: '300px',
            }}
          >
            Send
          </LoadingButton>
        </AnalyzerPane>
      </Stack>
      <Fade in={htmlAnalyze !== null && htmlAnalyze.errors.length > 0} timeout={750}>
        <Stack>
          <AnalyzerPane>
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
    </Stack>
  );
}

export default InputMode;
