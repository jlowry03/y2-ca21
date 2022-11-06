((csharp-mode . ((eval .
                       (add-hook 'after-save-hook
                                 #'(lambda () (untabify (point-min) (point-max))))))))
