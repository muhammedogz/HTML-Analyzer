import LoadingButton from '@mui/lab/LoadingButton';
import { FormLabel, Stack, Typography } from '@mui/material';
import AnalyzerPane from 'components/AnalyzerPane';
import Editor from 'components/Editor';
import Errors from 'components/Errors';
import { useCallback, useEffect, useState } from 'react';
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

  useEffect(() => {
    if (htmlAnalyze && loading) {
      setHtmlAnalyze(null);
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [loading]);

  return (
    <Stack gap={3} id="html-editor-stack">
      <Stack gap={3}>
        <AnalyzerPane gap={2}>
            <Typography fontWeight={700} fontSize="20px" pl="10px">
              Enter HTML Input
            </Typography>
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
      {htmlAnalyze && <Errors htmlAnalyze={htmlAnalyze} setHtmlAnalyze={setHtmlAnalyze} />}
    </Stack>
  );
}

export default InputMode;
