import { LoadingButton } from '@mui/lab';
import { Button, Stack, TextField, Typography } from '@mui/material';
import AnalyzerPane from 'components/AnalyzerPane';
import Editor from 'components/Editor';
import Errors from 'components/Errors';
import { useCallback, useState } from 'react';
import { getAnalyzeFromUrl, HTMlAnalyzerType } from 'src/fetchers/htmlAnalyzerFetchers';

const URLRegexp = /^(https?:\/\/)?([\da-z.-]+)\.([a-z.]{2,6})([/\w .-]*)*\/?$/;

const URLMode = () => {
  const [url, setUrl] = useState('');
  const [showEditor, setShowEditor] = useState(false);
  const [loading, setLoading] = useState(false);
  const [code, setCode] = useState('');
  const [htmlAnalyze, setHtmlAnalyze] = useState<HTMlAnalyzerType | null>(null);

  const getHtml = useCallback(async () => {
    try {
      setLoading(true);
      const response = await getAnalyzeFromUrl(url);
      if (response.status === 200) {
        setHtmlAnalyze(response.data);
        setCode(response.data.html);
        setLoading(false);
      }
    } catch (error) {
      console.error(error);
    }
  }, [url]);

  return (
    <Stack gap={4} minWidth={{ xs: '85vw', md: '600px' }}>
      <Stack gap={2}>
        <AnalyzerPane>
          <TextField label="URL" onChange={(e) => setUrl(e.target.value)} />
        </AnalyzerPane>
        <AnalyzerPane justifyContent="center" alignItems="center">
          <LoadingButton
            disabled={!URLRegexp.test(url)}
            loading={loading}
            onClick={getHtml}
            color="secondary"
            variant="contained"
            sx={{
              width: '300px',
            }}
          >
            Get HTML
          </LoadingButton>
        </AnalyzerPane>
      </Stack>
      {htmlAnalyze && <Errors htmlAnalyze={htmlAnalyze} setHtmlAnalyze={setHtmlAnalyze} />}
      {code.length > 0 && (
        <AnalyzerPane>
          <Button
            onClick={() => {
              setShowEditor(!showEditor);
            }}
          >
            <Typography>{showEditor ? 'Hide fetched HTML text' : 'Show fetched HTML'}</Typography>
          </Button>
        </AnalyzerPane>
      )}
      {showEditor && (
        <AnalyzerPane>
          <Editor code={code} setCode={setCode} initialReadOnly />
        </AnalyzerPane>
      )}
    </Stack>
  );
};

export default URLMode;
